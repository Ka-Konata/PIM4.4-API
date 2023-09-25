﻿namespace API.Data.ViewModels
{
    public class Secretario_Input
    {
        public string senha { get; set; }
        public string nome { get; set; }
        public Int64 cpf { get; set; }
        public Int64 rg { get; set; }
        public Int64? telefone { get; set; }
        public string? email { get; set; }
    }

    public class Secretario
    {
        public int id { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public Int64 cpf { get; set; }
        public Int64 rg { get; set; }
        public Int64? telefone { get; set; }
        public string? email { get; set; }
    }
}
