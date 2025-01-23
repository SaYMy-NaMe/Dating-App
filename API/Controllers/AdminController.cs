using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

public class AdminController : BaseApiController
{
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public ActionResult GetUsersWithRoles()
    {
        return Ok("Only Admin can see this");
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public ActionResult GetPhotosForModerations()
    {
        return Ok("Admin or Moderator can see this");
    }
}
