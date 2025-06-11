using AutoMapper;
using Necli.Aplicacion.DTOs;
using Necli.Entidades.Entities;

namespace Necli.LogicaNegocio.Perfiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRespuestaDTO>();
            CreateMap<UsuarioCrearDTO, Usuario>();
            CreateMap<UsuarioActualizarDTO, Usuario>();
        }
    }
}
