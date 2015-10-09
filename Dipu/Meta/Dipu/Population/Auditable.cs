//------------------------------------------------------------------------------------------------- 
// <copyright file="AuditableInterface.cs" company="inxin bvba">
// Copyright 2014-2015 inxin bvba.
// 
// Dual Licensed under
//   a) the Affero General Public Licence v3 (AGPL)
//   b) the Allors License
// 
// The AGPL License is included in the file LICENSE.
// The Allors License is an addendum to your contract.
// 
// Dipu is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// For more information visit http://www.dipu.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    #region Allors
    [Id("c27e19a3-8fa6-4424-96d8-8af7f77dbfd3")]
    #endregion
    [Inherit(typeof(AccessControlledObjectInterface))]
    public partial class AuditableInterface : Interface
    {
        #region Allors
        [Id("5b9252cf-7df0-4627-9f88-a0970ec5d546")]
        [AssociationId("7A5EBE38-7A69-405C-80F3-AF32E69EF212")]
        [RoleId("32D22464-6116-4615-9447-4B05DF408719")]
        #endregion
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType CreatedBy;

        #region Allors
        [Id("8916f4cb-fa5b-41b0-8b3a-018c1ace188d")]
        [AssociationId("34D89129-357A-4DE7-85EE-B76505BFB1C1")]
        [RoleId("504C3DB9-41CD-488E-95A9-B062F0380777")]
        #endregion
        [Type(typeof(AllorsDateTimeUnit))]
        public RelationType CreationDate;

        #region Allors
        [Id("6063cfe0-9fa6-4dbb-9d85-fdc4d90ccc4b")]
        [AssociationId("6450ACFA-AC53-435D-8B29-17BADD659E8E")]
        [RoleId("BBB130E1-041C-4CF9-9CD6-49DE2098792D")]
        #endregion
        [Indexed]
        [Type(typeof(UserInterface))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType LastModifiedBy;

        #region Allors
        [Id("5999f911-38aa-4a79-a07d-7ae64b170eb2")]
        [AssociationId("281DEF0D-06CC-498B-A414-8AD2E7860505")]
        [RoleId("46C29C20-670E-4455-B2FD-ED48ADDB4F5B")]
        #endregion
        [Type(typeof(AllorsDateTimeUnit))]
        public RelationType LastModifiedDate;

        public static AuditableInterface Instance { get; internal set; }

        internal AuditableInterface() : base(MetaPopulation.Instance)
        {
        }
    }
}