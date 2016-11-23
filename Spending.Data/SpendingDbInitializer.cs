using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using Spending.Data.DataModel;

namespace Spending.Data
{
    public class SpendingDbInitializer : CreateDatabaseIfNotExists<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            IList<Category> defaultCategories = new List<Category>();
            defaultCategories.Add(new Category
            {
                Title = "پوشاک"
            });
            defaultCategories.Add(new Category
            {
                Title = "ایاب و ذهاب"
            });
            defaultCategories.Add(new Category
            {
                Title = "تلفن همراه"
            });
            defaultCategories.Add(new Category
            {
                Title = "اینترنت"
            });

            context.Categories.AddRange(defaultCategories);

            base.Seed(context);
        }
    }
}