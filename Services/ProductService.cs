using System.Collections.Generic;
using cs68.Models;

namespace cs68.Services {
    public class ProductService : List<ProductModel> {
        public ProductService(){
            this.AddRange(new ProductModel[] {
                new ProductModel() {Id=1,Name="Sam sung m10",Price=2000},
                new ProductModel() {Id=2,Name="Iphone X",Price=3000},
                new ProductModel() {Id=3,Name="Iphone 6",Price=2500},
                new ProductModel() {Id=4,Name="Nokia",Price=1000}

            });
        }
    }
}