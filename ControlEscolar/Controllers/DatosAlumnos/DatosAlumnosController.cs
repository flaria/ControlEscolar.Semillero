﻿using ControlEscolar.Models;
using ControlEscolar.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControlEscolar.Controllers.DatosAlumnos
{
    public class DatosAlumnosController : ApiController
    {
        // GET: api/DatosAlumnos
       /* public c_DatosAlumnos[] Get()
        {
            List<c_DatosAlumnos> datosalumno = new List<c_DatosAlumnos>();
            using (CursoEscolarEntities contexto = new CursoEscolarEntities())
            {
                foreach (Models.Alumno item in contexto.Alumno)
                {
                    datosalumno.Add(new c_DatosAlumnos()
                    {
                        Id = item.Al_Id,
                        Nombre = item.Al_Nombre,
                        Apellidos = item.Al_Apellido,
                        Carrera = item.Al_Carrera_Id,
                        Semestres = item.Al_Semestre
                    });
                }
            }
            return datosalumno.ToArray();
        }
        */
        public tab_DatosAlumnos[] GET()
        {
            List<tab_DatosAlumnos> LAlum = new List<tab_DatosAlumnos>();
            using (CursoEscolarEntities contexto = new CursoEscolarEntities())
            {
                var query = from alu in contexto.Alumno
                            join car in contexto.Carrera on alu.Al_Carrera_Id equals car.Car_Id
                            select new tab_DatosAlumnos
                            {
                                Id = alu.Al_Id,
                                Nombre = alu.Al_Nombre,
                                Apellidos = alu.Al_Apellido,
                                Carrera = car.Car_Nombre,
                                Semestres = alu.Al_Semestre
                            };
                LAlum = query.ToList();
            }
            return LAlum.ToArray();
        }

        public bool Post(c_DatosAlumnos al)
        {
            Boolean guardar = false;
            using (CursoEscolarEntities contexto = new CursoEscolarEntities())
            {
                Alumno alumno = new Alumno
                {
                    Al_Nombre = al.Nombre,
                    Al_Apellido = al.Apellidos,
                    Al_Carrera_Id = al.Carrera,
                    Al_Semestre = al.Semestres
                };
                contexto.Alumno.Add(alumno);
                contexto.SaveChanges();
                guardar = true;
            }
            return guardar;
        }
    }
}
