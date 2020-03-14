using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Graph;

namespace MsGraphMdArafat.Models
{
  public class SingleGroup
  {
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IGroupOwnersCollectionWithReferencesPage Owner { get; set; }
    public IGroupMembersCollectionWithReferencesPage Member { get; set; }
  }
}
