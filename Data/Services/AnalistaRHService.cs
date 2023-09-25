﻿using API.DataBase;
using API.Data.ViewModels;
using API.Data.Models;

namespace API.Data.Services
{
    public class AnalistaRHService
    {
        private readonly DBContext context;
        public AnalistaRHService(DBContext context)
        {
            this.context = context;
        }

        public List<ViewModels.AnalistaRH> GetAllAnalistasRH(string? nome)
        {
            var response = this.context.AnalistasRH
                .Where(nome != null ? n => n.nome.Contains(nome) : n => n.id != 0)
                .Select(analistaRH => new ViewModels.AnalistaRH()
                {
                    id = analistaRH.id,
                    senha = analistaRH.senha,
                    nome = analistaRH.nome,
                    cpf = analistaRH.cpf,
                    rg = analistaRH.rg,
                    telefone = analistaRH.telefone,
                    email = analistaRH.email
                }).ToList();
            return response;
        }

        public ViewModels.AnalistaRH GetAnalistaRH(int id)
        {
            var response = this.context.AnalistasRH
                .Where(n => n.id == id)
                .Select(analistaRH => new ViewModels.AnalistaRH()
                {
                    id = analistaRH.id,
                    senha = analistaRH.senha,
                    nome = analistaRH.nome,
                    cpf = analistaRH.cpf,
                    rg = analistaRH.rg,
                    telefone = analistaRH.telefone,
                    email = analistaRH.email
                }).FirstOrDefault();

            return response;
        }

        public void AddAnalistaRH(AnalistaRH_Input analistaRH)
        {
            var obj = new Models.AnalistaRH()
            {
                senha = analistaRH.senha,
                nome = analistaRH.nome,
                cpf = analistaRH.cpf,
                rg = analistaRH.rg,
                telefone = analistaRH.telefone,
                email = analistaRH.email
            };

            this.context.Add(obj);
            this.context.SaveChanges();
        }

        public void UpdateAnalistaRH(int id, AnalistaRH_Input analistaRH)
        {
            var obj = this.context.AnalistasRH.FirstOrDefault(n => n.id == id);

            obj.senha = analistaRH.senha;
            obj.nome = analistaRH.nome;
            obj.cpf = analistaRH.cpf;
            obj.rg = analistaRH.rg;
            obj.telefone = analistaRH.telefone;
            obj.email = analistaRH.email;

            this.context.SaveChanges();
        }

        public void RemoveAnalistaRH(Models.AnalistaRH analistaRH)
        {
            this.context.Remove(analistaRH);
            this.context.SaveChanges();
        }
    }
}