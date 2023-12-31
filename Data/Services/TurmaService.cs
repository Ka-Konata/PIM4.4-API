﻿using API.DataBase;
using API.Data.ViewModels;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Data.Services
{
    public class TurmaService
    {
        private readonly DBContext context;
        public TurmaService(DBContext context)
        {
            this.context = context;
        }

        public List<ViewModels.Turma> ServiceGetAll(string? nome)
        {
            var response = this.context.Turmas
                .Where(nome != null ? n => n.nome.Contains(nome) : n => n.id != 0)
                .Select(turma => new ViewModels.Turma()
                {
                    id = turma.id,
                    nome = turma.nome,
                    curso = this.context.Cursos
                        .Where(n => n.id == turma.idCurso)
                        .Select(curso => new ViewModels.Curso()
                        {
                            id = curso.id,
                            nome = curso.nome,
                            cargaHoraria = curso.cargaHoraria,
                            aulasTotais = curso.aulasTotais
                        }).FirstOrDefault()
                }).ToList();

            foreach (var r in response)
            {
                List<Models.CursoMatriculado> cursosMatriculados = this.context.CursoMatriculados
                .Where(e => e.idTurma == r.id).ToList();

                if (cursosMatriculados.Count > 0)
                {
                    r.alunos = new List<ViewModels.Aluno?>();
                    foreach (var cm in cursosMatriculados)
                    {
                        r.alunos.Add(this.context.Alunos
                        .Where(n => n.id == cm.idAluno)
                        .Select(aluno => new ViewModels.Aluno()
                        {
                            id = aluno.id,
                            nome = aluno.nome,
                            cpf = aluno.cpf,
                            rg = aluno.rg,
                            telefone = aluno.telefone,
                            email = aluno.email,
                            cargo = aluno.cargo,
                            cursos = this.context.CursoMatriculados
                            .Where(n => n.idAluno == aluno.id)
                            .Select(cursoMatriculado => new ViewModels.CursoMatriculado()
                            {
                                id = cursoMatriculado.id,
                                semestreAtual = cursoMatriculado.semestreAtual,
                                trancado = cursoMatriculado.trancado,
                                finalizado = cursoMatriculado.finalizado,
                                turma = this.context.Turmas
                                .Where(n => n.id == cursoMatriculado.idTurma)
                                .Select(turma => new ViewModels.Turma()
                                {
                                    id = turma.id,
                                    nome = turma.nome,
                                    curso = this.context.Cursos
                                    .Where(n => n.id == turma.idCurso)
                                    .Select(curso => new ViewModels.Curso()
                                    {
                                        id = curso.id,
                                        nome = curso.nome,
                                        cargaHoraria = curso.cargaHoraria,
                                        aulasTotais = curso.aulasTotais
                                    }).FirstOrDefault()
                                }).FirstOrDefault(),
                                curso = this.context.Cursos
                                .Where(n => n.id == cursoMatriculado.idCurso)
                                .Select(curso => new ViewModels.Curso()
                                {
                                    id = curso.id,
                                    nome = curso.nome,
                                    cargaHoraria = curso.cargaHoraria,
                                    aulasTotais = curso.aulasTotais
                                }).FirstOrDefault()
                            }).ToList()
                        }).FirstOrDefault());
                    }
                }

                List<Models.DisciplinaMinistrada> disciplinaMinistradas = this.context.DisciplinaMinistradas
                .Where(e => e.idTurma == r.id).ToList();

                if (disciplinaMinistradas.Count > 0)
                {
                    r.professores = new List<ViewModels.Professor?>();
                    foreach (var dm in disciplinaMinistradas)
                    {
                        var toAdd = this.context.Professores
                        .Where(n => n.id == dm.idProfessor)
                        .Select(professor => new ViewModels.Professor()
                        {
                            id = professor.id,
                            nome = professor.nome,
                            cpf = professor.cpf,
                            rg = professor.rg,
                            telefone = professor.telefone,
                            email = professor.email,
                            cargo = professor.cargo,
                            disciplinasMinistradas = this.context.DisciplinaMinistradas
                            .Where(n => n.idProfessor == professor.id)
                            .Select(disciplinaMinistrada => new ViewModels.DisciplinaMinistrada()
                            {
                                id = disciplinaMinistrada.id,
                                disciplina = this.context.Disciplinas
                                    .Where(n => n.id == disciplinaMinistrada.idDisciplina)
                                    .Select(disciplina => new ViewModels.Disciplina()
                                    {
                                        id = disciplina.id,
                                        nome = disciplina.nome
                                    }).FirstOrDefault(),
                                turma = this.context.Turmas
                                    .Where(n => n.id == disciplinaMinistrada.idTurma)
                                    .Select(turma => new ViewModels.Turma()
                                    {
                                        id = turma.id,
                                        nome = turma.nome,
                                        curso = this.context.Cursos
                                        .Where(n => n.id == turma.idCurso)
                                        .Select(curso => new ViewModels.Curso()
                                        {
                                            id = curso.id,
                                            nome = curso.nome,
                                            cargaHoraria = curso.cargaHoraria,
                                            aulasTotais = curso.aulasTotais
                                        }).FirstOrDefault()
                                    }).FirstOrDefault(),
                                coordenador = disciplinaMinistrada.coordenador
                            }).ToList(),
                        }).FirstOrDefault();

                        r.professores.Add(toAdd);
                        if (dm.coordenador == true)
                        {
                            r.coordenador = toAdd;
                        }
                    }
                }
            }

            return response;
        }

        public ViewModels.Turma ServiceGet(int id)
        {
            var response = this.context.Turmas
                .Where(n => n.id == id)
                .Select(turma => new ViewModels.Turma()
                {
                    id = turma.id,
                    nome = turma.nome,
                    curso = this.context.Cursos
                    .Where(n => n.id == turma.idCurso)
                    .Select(curso => new ViewModels.Curso()
                    {
                        id = curso.id,
                        nome = curso.nome,
                        cargaHoraria = curso.cargaHoraria,
                        aulasTotais = curso.aulasTotais
                    }).FirstOrDefault()
                }).FirstOrDefault();

            List<Models.CursoMatriculado>? cursosMatriculados = this.context.CursoMatriculados
            .Where(e => e.idTurma == response.id).ToList();

            if (cursosMatriculados.Count > 0)
            {
                response.alunos = new List<ViewModels.Aluno?>();
                foreach (var cm in cursosMatriculados)
                {
                    response.alunos.Add(this.context.Alunos
                    .Where(n => n.id == cm.idAluno)
                    .Select(aluno => new ViewModels.Aluno()
                    {
                        id = aluno.id,
                        nome = aluno.nome,
                        cpf = aluno.cpf,
                        rg = aluno.rg,
                        telefone = aluno.telefone,
                        email = aluno.email,
                        cargo = aluno.cargo,
                        cursos = this.context.CursoMatriculados
                        .Where(n => n.idAluno == aluno.id)
                        .Select(cursoMatriculado => new ViewModels.CursoMatriculado()
                        {
                            id = cursoMatriculado.id,
                            semestreAtual = cursoMatriculado.semestreAtual,
                            trancado = cursoMatriculado.trancado,
                            finalizado = cursoMatriculado.finalizado,
                            turma = this.context.Turmas
                            .Where(n => n.id == cursoMatriculado.idTurma)
                            .Select(turma => new ViewModels.Turma()
                            {
                                id = turma.id,
                                nome = turma.nome,
                                curso = this.context.Cursos
                                .Where(n => n.id == turma.idCurso)
                                .Select(curso => new ViewModels.Curso()
                                {
                                    id = curso.id,
                                    nome = curso.nome,
                                    cargaHoraria = curso.cargaHoraria,
                                    aulasTotais = curso.aulasTotais
                                }).FirstOrDefault()
                            }).FirstOrDefault(),
                            curso = this.context.Cursos
                            .Where(n => n.id == cursoMatriculado.idCurso)
                            .Select(curso => new ViewModels.Curso()
                            {
                                id = curso.id,
                                nome = curso.nome,
                                cargaHoraria = curso.cargaHoraria,
                                aulasTotais = curso.aulasTotais
                            }).FirstOrDefault()
                        }).ToList()
                    }).FirstOrDefault());
                }
            }

            List<Models.DisciplinaMinistrada> disciplinaMinistradas = this.context.DisciplinaMinistradas
            .Where(e => e.idTurma == response.id).ToList();

            if (disciplinaMinistradas.Count > 0)
            {
                response.professores = new List<ViewModels.Professor?>();
                foreach (var dm in disciplinaMinistradas)
                {
                    var toAdd = this.context.Professores
                    .Where(n => n.id == dm.idProfessor)
                    .Select(professor => new ViewModels.Professor()
                    {
                        id = professor.id,
                        nome = professor.nome,
                        cpf = professor.cpf,
                        rg = professor.rg,
                        telefone = professor.telefone,
                        email = professor.email,
                        cargo = professor.cargo,
                        disciplinasMinistradas = this.context.DisciplinaMinistradas
                        .Where(n => n.idProfessor == professor.id)
                        .Select(disciplinaMinistrada => new ViewModels.DisciplinaMinistrada()
                        {
                            id = disciplinaMinistrada.id,
                            disciplina = this.context.Disciplinas
                                .Where(n => n.id == disciplinaMinistrada.idDisciplina)
                                .Select(disciplina => new ViewModels.Disciplina()
                                {
                                    id = disciplina.id,
                                    nome = disciplina.nome
                                }).FirstOrDefault(),
                            turma = this.context.Turmas
                                .Where(n => n.id == disciplinaMinistrada.idTurma)
                                .Select(turma => new ViewModels.Turma()
                                {
                                    id = turma.id,
                                    nome = turma.nome,
                                    curso = this.context.Cursos
                                    .Where(n => n.id == turma.idCurso)
                                    .Select(curso => new ViewModels.Curso()
                                    {
                                        id = curso.id,
                                        nome = curso.nome,
                                        cargaHoraria = curso.cargaHoraria,
                                        aulasTotais = curso.aulasTotais
                                    }).FirstOrDefault()
                                }).FirstOrDefault(),
                            coordenador = disciplinaMinistrada.coordenador
                        }).ToList(),
                    }).FirstOrDefault();

                    response.professores.Add(toAdd);
                    if (dm.coordenador == true)
                    {
                        response.coordenador = toAdd;
                    }
                }
            }

            return response;
        }

        public void ServicePost(Turma_Input turma)
        {
            Turma_Input Turma = turma;
            var obj = new Models.Turma()
            {
                nome = turma.nome,
                idCurso = turma.idCurso
            };

            this.context.Add(obj);
            this.context.SaveChanges();
        }

        public void ServicePut(int id, Turma_Input turma)
        {
            var obj = this.context.Turmas.FirstOrDefault(n => n.id == id);

            obj.nome = turma.nome;
            obj.idCurso = turma.idCurso;

            this.context.SaveChanges();
        }

        public void ServiceDelete(Models.Turma turma)
        {
            this.context.Remove(turma);
            this.context.SaveChanges();
        }

        public void ServiceAddAluno(Models.Aluno aluno)
        {

        }
    }
}
