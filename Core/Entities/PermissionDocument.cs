using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class PermissionDocument
	{
		public int Id { get; set; }
		public string EmployeeForename { get; set; } = string.Empty;
		public string EmployeeSurname { get; set; } = string.Empty;
		public int PermissionType { get; set; }
		public DateTime PermissionDate { get; set; }
	}
}
