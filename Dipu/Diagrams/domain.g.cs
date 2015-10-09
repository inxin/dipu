namespace Allors.Domain
{
		public interface AccessControlledObject  : Object 
		{
						Permission DeniedPermissions {set;}

						SecurityToken SecurityTokens {set;}

		}
		public interface Commentable  : Object 
		{
						global::System.String Comment {set;}

		}
		public interface Deletable  : Object 
		{
		}
		public interface Enumeration  : Object, AccessControlledObject, UniquelyIdentifiable 
		{
						LocalisedText LocalisedNames {set;}

						global::System.String Name {set;}

						global::System.Boolean IsActive {set;}

		}
		public interface Localised  : Object 
		{
						Locale Locale {set;}

		}
		public interface Object 
		{
		}
		public interface ObjectState  : Object, UniquelyIdentifiable 
		{
						Permission DeniedPermissions {set;}

						global::System.String Name {set;}

		}
		public interface Period  : Object 
		{
						global::System.DateTime FromDate {set;}

						global::System.DateTime? ThroughDate {set;}

		}
		public interface Printable  : Object, AccessControlledObject, UniquelyIdentifiable 
		{
						global::System.String PrintContent {set;}

		}
		public interface SecurityTokenOwner  : Object 
		{
						SecurityToken OwnerSecurityToken {set;}

		}
		public interface Transitional  : Object, AccessControlledObject 
		{
						ObjectState PreviousObjectState {set;}

						ObjectState LastObjectState {set;}

		}
		public interface UniquelyIdentifiable  : Object 
		{
						global::System.Guid UniqueId {set;}

		}
		public interface User  : Object, SecurityTokenOwner, AccessControlledObject, Localised 
		{
						global::System.Boolean? UserEmailConfirmed {set;}

						global::System.String UserName {set;}

						global::System.String UserEmail {set;}

						global::System.String UserPasswordHash {set;}

		}
		public interface Auditable  : Object, AccessControlledObject 
		{
						User CreatedBy {set;}

						global::System.DateTime? CreationDate {set;}

						User LastModifiedBy {set;}

						global::System.DateTime? LastModifiedDate {set;}

		}
		public interface AccessControl  : Object, Deletable, AccessControlledObject 
		{
						UserGroup SubjectGroups {set;}

						User Subjects {set;}

						SecurityToken Objects {set;}

						Role Role {set;}

						global::System.Guid CacheId {set;}

		}
		public interface Counter  : Object, UniquelyIdentifiable 
		{
						global::System.Int32 Value {set;}

		}
		public interface Country  : Object, AccessControlledObject 
		{
						Currency Currency {set;}

						global::System.String Name {set;}

						LocalisedText LocalisedNames {set;}

						global::System.String IsoCode {set;}

		}
		public interface Currency  : Object, AccessControlledObject 
		{
						global::System.String IsoCode {set;}

						global::System.String Name {set;}

						global::System.String Symbol {set;}

						LocalisedText LocalisedNames {set;}

		}
		public interface Image  : Object, Deletable 
		{
						Media Original {set;}

						Media Responsive {set;}

						global::System.String OriginalFilename {set;}

						Media Thumbnail {set;}

		}
		public interface Language  : Object, AccessControlledObject 
		{
						global::System.String Name {set;}

						global::System.String IsoCode {set;}

						LocalisedText LocalisedNames {set;}

		}
		public interface Locale  : Object, AccessControlledObject 
		{
						global::System.String Name {set;}

						Language Language {set;}

						Country Country {set;}

		}
		public interface LocalisedText  : Object, AccessControlledObject, Localised 
		{
						global::System.String Text {set;}

		}
		public interface Login  : Object, Deletable 
		{
						global::System.String Key {set;}

						global::System.String Provider {set;}

						User User {set;}

		}
		public interface Media  : Object, UniquelyIdentifiable, AccessControlledObject, Deletable 
		{
						MediaType MediaType {set;}

						MediaContent MediaContent {set;}

		}
		public interface MediaContent  : Object, Deletable 
		{
						global::System.Byte[] Value {set;}

						global::System.String Hash {set;}

		}
		public interface MediaType  : Object, AccessControlledObject 
		{
						global::System.String DefaultFileExtension {set;}

						global::System.String Name {set;}

		}
		public interface Permission  : Object, Deletable, AccessControlledObject 
		{
						global::System.Guid OperandTypePointer {set;}

						global::System.Guid ConcreteClassPointer {set;}

						global::System.Int32 OperationEnum {set;}

		}
		public interface Person  : Object, User, AccessControlledObject, UniquelyIdentifiable 
		{
						global::System.String FirstName {set;}

						global::System.String LastName {set;}

						global::System.String MiddleName {set;}

		}
		public interface PrintQueue  : Object, AccessControlledObject, UniquelyIdentifiable 
		{
						Printable Printables {set;}

						global::System.String Name {set;}

		}
		public interface Role  : Object, AccessControlledObject, UniquelyIdentifiable 
		{
						Permission Permissions {set;}

						global::System.String Name {set;}

		}
		public interface SecurityToken  : Object, Deletable 
		{
		}
		public interface Singleton  : Object, AccessControlledObject 
		{
						PrintQueue DefaultPrintQueue {set;}

						Locale DefaultLocale {set;}

						Locale Locales {set;}

						SecurityToken AdministratorSecurityToken {set;}

						User Guest {set;}

						SecurityToken DefaultSecurityToken {set;}

		}
		public interface StringTemplate  : Object, UniquelyIdentifiable, Localised 
		{
						global::System.String Body {set;}

						global::System.String Name {set;}

		}
		public interface Transition  : Object 
		{
						ObjectState FromStates {set;}

						ObjectState ToState {set;}

		}
		public interface UserGroup  : Object, UniquelyIdentifiable, AccessControlledObject 
		{
						Role Role {set;}

						User Members {set;}

						UserGroup Parent {set;}

						global::System.String Name {set;}

		}
}