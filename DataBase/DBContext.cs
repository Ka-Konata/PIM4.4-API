﻿using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataBase
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<AnalistaRH> AnalistasRH { get; set; }
        public DbSet<Secretario> Secretarios { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<DisciplinaMinistrada> DisciplinaMinistradas { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<CursoMatriculado> CursoMatriculados { get; set; }
        public DbSet<DisciplinaCursada> DisciplinaCursadas { get; set; }
    }
}
