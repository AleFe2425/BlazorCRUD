﻿namespace BlazorCRUD.Models
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }    
        public double Precio { get; set; }
        public int Qty { get; set; }

    }
}
