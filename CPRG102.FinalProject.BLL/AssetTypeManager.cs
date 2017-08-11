using CPRG102.FinalProject.Data;
using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.BLL
{
    public class AssetTypeManager
    {
        AssetContext context = new AssetContext();

        public IEnumerable<AssetType> GetAssetTypes()
        {
            var AssetTypes = context.AssetTypes.ToList();
            return AssetTypes;
        }

        public void AddAssetType(AssetType AssetType)
        {
            context.AssetTypes.Add(AssetType);
            context.SaveChanges();
        }

        public void UpdateAssetType(AssetType AssetType)
        {
            var type = context.AssetTypes.Find(AssetType.Id);
            type.Name = AssetType.Name;
            context.SaveChanges();
        }

        public AssetType Find(int id)
        {
            var type = context.AssetTypes.Find(id);
            return type;
        }
    }
}
