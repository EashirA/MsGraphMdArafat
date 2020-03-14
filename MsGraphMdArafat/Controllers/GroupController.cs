using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MsGraphMdArafat.Services;

namespace MsGraphMdArafat.Controllers
{
    public class GroupController : BaseController
    {
    // GET: Groups
    public async Task<ActionResult> GroupDetail()
    {
      var groups = await GroupService.GetGroupsAsync();
      return View(groups);
    }

    public async Task<ActionResult> GetTheGroup( string id)
    {
      var group = await GroupService.GetSpecificGroupDetail(id);
      return View(group);
    }

    public ActionResult Error(string message, string debug)
    {
      Flash(message, debug);
      return RedirectToAction("GroupDetail");
    }
  }
}
