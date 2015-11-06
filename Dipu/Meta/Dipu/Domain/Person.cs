namespace Allors.Meta
{
    [Inherit(typeof(AuditableInterface))]
    public partial class PersonClass
    {
        internal override void DipuExtend()
        {
            this.FirstName.AddGroup(Groups.Workspace);
            this.LastName.AddGroup(Groups.Workspace);
        }
    }
}