using CPRG102.FinalProject.Data;
using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.BLL
{
    public class AssetManager
    {
        AssetContext context = new AssetContext();

        public IEnumerable<Asset> GetAssets()
        {
            var Assets = context.Assets.ToList();
            return Assets;
        }

        public void AddAsset(Asset Asset)
        {
            context.Assets.Add(Asset);
            context.SaveChanges();
        }

        public void UpdateAsset(Asset Asset)
        {
            var asset = context.Assets.Find(Asset.Id);
            asset.TagNumber = Asset.TagNumber;
            asset.AssetTypeId = Asset.AssetTypeId;
            asset.ManufacturerId = Asset.ManufacturerId;
            asset.ModelId = Asset.ModelId;
            asset.Description = Asset.Description;
            asset.AssignedTo = Asset.AssignedTo;
            asset.SerialNumber = Asset.SerialNumber;
            context.SaveChanges();
        }

        public Asset Find(int id)
        {
            var asset = context.Assets.Find(id);
            return asset;
        }

        public IEnumerable<Asset> GetAssetByEmployee(string EmployeeNumber)
        {
            var assets = context.Assets.Where(o => o.AssignedTo == EmployeeNumber).ToList();
            return assets;
        }

        public IEnumerable<Asset> AssetsByType(int id)
        {
            var assets = context.Assets.Where(o => o.AssetTypeId == id).ToList();
            return assets;
        }
    }
}
