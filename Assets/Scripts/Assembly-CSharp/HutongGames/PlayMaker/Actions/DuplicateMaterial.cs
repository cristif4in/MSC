namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("♥ Caligula ♥")]
	[Tooltip("Duplicates a material and renames it.")]
	public class DuplicateMaterial : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Materials new name")]
		public FsmString material_name;

		[Tooltip("Material to duplicate")]
		[RequiredField]
		public FsmMaterial material;

		[Tooltip("New material variable")]
		[RequiredField]
		public FsmMaterial new_material;

		public override void OnEnter()
		{
			Finish();
		}

		private FsmMaterial doNew(FsmMaterial vanha)
		{
			return new FsmMaterial(vanha);
		}

		private void setName(FsmMaterial matsku, string name)
		{
			matsku.Name = name;
		}
	}
}
