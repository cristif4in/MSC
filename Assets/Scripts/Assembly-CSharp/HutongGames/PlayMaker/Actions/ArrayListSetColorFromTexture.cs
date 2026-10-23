using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11929.0")]
	[Tooltip("Create color palette from texture for Array. Texture has to be read/write enabled.")]
	public class ArrayListSetColorFromTexture : ArrayListActions
	{
		[RequiredField]
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[Tooltip("set alpha")]
		[HasFloatSlider(0f, 1f)]
		[ActionSection("Option")]
		public FsmBool disableAlpha;

		[ActionSection("Input")]
		public FsmTexture texture;

		[ActionSection("Event")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent doneEvent;

		[Tooltip("The event if error")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent failureEvent;

		private Color colorSet;

		private Color colorTemp;

		private Color[] pix;

		private List<Color> finalPix;

		private Texture2D targetmaskedTexture;

		private Color colorAtSource;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			failureEvent = null;
			doneEvent = null;
			texture = null;
			disableAlpha = false;
		}

		public override void OnEnter()
		{
			bool flag = false;
			if (texture.IsNone)
			{
				Debug.LogWarning("<color=#6B8E23ff>No texture. Please review!</color>", base.Owner);
				flag = true;
			}
			targetmaskedTexture = null;
			targetmaskedTexture = (Texture2D)texture.Value;
			try
			{
				targetmaskedTexture.GetPixel(0, 0);
			}
			catch (UnityException)
			{
				Debug.LogWarning("<color=#6B8E23ff>Please enable read/write on texture  </color>[" + texture.Value.name + "]", base.Owner);
				flag = true;
			}
			if (flag)
			{
				base.Fsm.Event(failureEvent);
				Finish();
			}
			else if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				DoArraySetColor();
			}
			base.Fsm.Event(doneEvent);
			Finish();
		}

		public void DoArraySetColor()
		{
			if (!isProxyValid())
			{
				base.Fsm.Event(failureEvent);
				return;
			}
			pix = targetmaskedTexture.GetPixels(0);
			if (disableAlpha.Value)
			{
				for (int i = 0; i < pix.Length; i++)
				{
					pix[i].a = 1f;
				}
			}
			finalPix = new List<Color>();
			finalPix.Add(pix[0]);
			for (int j = 0; j < pix.Length; j++)
			{
				if (!disableAlpha.Value)
				{
					colorAtSource = ClearRGBIfNoAlpha(pix[j]);
				}
				else
				{
					colorAtSource = pix[j];
				}
				if (!ContainsColor(colorAtSource))
				{
					finalPix.Add(colorAtSource);
				}
			}
			proxy.arrayList.Clear();
			for (int k = 0; k < finalPix.Count; k++)
			{
				proxy.arrayList.Add(finalPix[k]);
			}
			finalPix = new List<Color>();
			pix = new Color[0];
			base.Fsm.Event(doneEvent);
		}

		private static Color ClearRGBIfNoAlpha(Color colorToClear)
		{
			Color result = colorToClear;
			if (Mathf.Approximately(result.a, 0f))
			{
				result = Color.clear;
			}
			return result;
		}

		public bool ContainsColor(Color colorToFind)
		{
			return IndexOf(colorToFind) >= 0;
		}

		public int IndexOf(Color colorToFind)
		{
			int result = -1;
			for (int i = 0; i < finalPix.Count; i++)
			{
				bool flag = Mathf.Approximately(colorToFind.a, 0f);
				bool flag2 = Mathf.Approximately(finalPix[i].a, 0f);
				if ((flag && flag2) || finalPix[i] == colorToFind)
				{
					result = i;
					break;
				}
			}
			return result;
		}
	}
}
