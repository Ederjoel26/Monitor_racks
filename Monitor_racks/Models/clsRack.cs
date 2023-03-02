using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_racks.Models
{
    public static class clsRack
    {
        public static void EmpezarEscuchar(string sSiteName, string sRackName, List<Label> oRackString, List<Image> oImgRack, bool fRack, double width = 0, double height = 0)
        {
            CrossCloudFirestore
               .Current
               .Instance
               .Collection(sSiteName)
               .Document(sRackName)
               .AddSnapshotListener((snap, error) =>
               {
                   clsRackModel oRack = snap.ToObject<clsRackModel>();
                   clsAsignar.Asignacion(oRack, oRackString, oImgRack, fRack, width, height);
               });
        }
    }
}
