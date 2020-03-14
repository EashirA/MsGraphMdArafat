using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Graph;
using MsGraphMdArafat.Helpers;
using MsGraphMdArafat.ViewModels;
using MsGraphMdArafat.Models;
using MyGroup = MsGraphMdArafat.Models.Group;

namespace MsGraphMdArafat.Services
{
  public class GroupService
  {
    public static async Task<GroupViewModel> GetGroupsAsync()
    {
      GraphServiceClient graphClient = GraphHelper.GetAuthenticatedClient();

      var groupsViewModel = new GroupViewModel();

      var groups = await graphClient.Groups.Request().GetAsync();

      foreach (var item in groups)
      {
        var groupModel = new MyGroup();

        var photo = await graphClient.Groups[item.Id].Photo.Content.Request().GetAsync();
        var ms = new MemoryStream();
        photo.CopyTo(ms);
        var buffer = ms.ToArray();

        groupModel.Logo = Convert.ToBase64String(buffer);
        groupModel.Name = item.DisplayName;
        groupModel.Id = item.Id;

        groupsViewModel.GroupList.Add(groupModel);
      }
      return groupsViewModel;
    }

    public static async Task<SingleGroup> GetSpecificGroupDetail(string id)
    {
      GraphServiceClient graphClient = GraphHelper.GetAuthenticatedClient();
      var theGroup = new SingleGroup();
      var group = await graphClient.Groups[id].Request().GetAsync();

      var photo = await graphClient.Groups[id].Photo.Content.Request().GetAsync();
      var ms = new MemoryStream();
      photo.CopyTo(ms);
      var buffer = ms.ToArray();

      theGroup.Logo = Convert.ToBase64String(buffer);
      theGroup.Name = group.DisplayName;
      theGroup.Description = group.Description;
      theGroup.Owner = group.Owners;
      theGroup.Member = group.Members;

      return theGroup;
    }
  }
}
