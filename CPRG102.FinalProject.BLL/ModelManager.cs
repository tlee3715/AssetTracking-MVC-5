using CPRG102.FinalProject.Data;
using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.BLL
{
    public class ModelManager
    {
        AssetContext context = new AssetContext();

        public IEnumerable<Model> GetModels()
        {
            var models = context.Models.ToList();
            return models;
        }

        public void AddModel(Model model)
        {
            context.Models.Add(model);
            context.SaveChanges(); //persist to database
        }

        public void UpdateModel(Model model)
        {
            var mod = context.Models.Find(model.Id);
            mod.Name = model.Name;
            mod.ManufacturerId = model.ManufacturerId;
            context.SaveChanges(); //persist to database
        }

        public Model Find(int id)
        {
            var model = context.Models.Find(id);
            return model;
        }
    }
}
