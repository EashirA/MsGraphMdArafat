using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MsGraphMdArafat.Models;

namespace MsGraphMdArafat.ViewModels
{
  public class GroupViewModel
  {
    public GroupViewModel()
    {
      GroupList = new List<Group>();
    }
    public List<Group> GroupList { get; set; }
  }
}
