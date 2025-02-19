using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class PermissionsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public PermissionsIntegrationTests(WebApplicationFactory<Program> factory)
	{
		_client = factory.CreateClient();
	}

	[Fact]
	public async Task RequestPermission_ShouldReturnSuccess()
	{
		var request = new
		{
			EmployeeForename = "Roman",
			EmployeeSurname = "Veron",
			PermissionType = 1,
			PermissionDate = DateTime.UtcNow
		};

		var response = await _client.PostAsJsonAsync("/api/Permissions/Request", request);

		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadFromJsonAsync<dynamic>();
		Assert.NotNull(result);
		Assert.True(result.Id > 0);
	}

	[Fact]
	public async Task ModifyPermission_ShouldReturnSuccess_WhenPermissionExists()
	{
		var modifyRequest = new
		{
			Id = 1,
			EmployeeForename = "Roman",
			EmployeeSurname = "Veron",
			PermissionType = 2,
			PermissionDate = DateTime.UtcNow
		};

		var response = await _client.PutAsJsonAsync("/api/Permissions/Modify/1", modifyRequest);

		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadAsStringAsync();
		Assert.Contains("successfully", result);
	}

	[Fact]
	public async Task GetPermissions_ShouldReturnPermissions()
	{
		var response = await _client.GetAsync("/api/Permissions/Get");

		response.EnsureSuccessStatusCode();
		var permissions = await response.Content.ReadFromJsonAsync<dynamic>();
		Assert.NotNull(permissions);
	}
}