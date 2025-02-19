namespace Core.Entities
{
	public class Permission
	{
		public int Id { get; set; }
		public string EmployeeForename { get; set; } = string.Empty;
		public string EmployeeSurname { get; set; } = string.Empty;
		public int PermissionType { get; set; }
		public DateTime PermissionDate { get; set; }

		public PermissionType? PermissionTypeNavigation { get; set; }
	}
}
