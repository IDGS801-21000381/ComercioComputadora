using ComercioComputadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioComputadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputadorasController : ControllerBase
    {
        private readonly ComercioComputadoraContext _context;

        public ComputadorasController(ComercioComputadoraContext context)
        {
            _context = context;
        }

        // Método GET ListaComputadoras que devuelve la lista de todas las computadoras en la BD
        [HttpGet]
        [Route("ListaComputadoras")]
        public async Task<IActionResult> Lista()
        {
            var listaComputadoras = await _context.Computadoras.ToListAsync();
            return Ok(listaComputadoras);
        }

        // Método POST AgregarComputadora que agrega una nueva computadora a la BD
        [HttpPost]
        [Route("AgregarComputadora")]
        public async Task<IActionResult> Agregar([FromBody] Computadora request)
        {
            await _context.Computadoras.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        // Método PUT ModificarComputadora que actualiza una computadora existente en la BD
        [HttpPut]
        [Route("ModificarComputadora/{id:int}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] Computadora request)
        {
            var computadoraModificar = await _context.Computadoras.FindAsync(id);

            if (computadoraModificar == null)
            {
                return BadRequest("No existe la computadora");
            }

            computadoraModificar.Categoria = request.Categoria;
            computadoraModificar.Nombre = request.Nombre;
            computadoraModificar.Descripcion = request.Descripcion;
            computadoraModificar.Imagen = request.Imagen;
            computadoraModificar.Precio = request.Precio;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la computadora: {ex.Message}");
            }

            return Ok("Computadora actualizada exitosamente");
        }

        // Método DELETE EliminarComputadora que elimina una computadora de la BD
        [HttpDelete]
        [Route("EliminarComputadora/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var computadoraEliminar = await _context.Computadoras.FindAsync(id);

            if (computadoraEliminar == null)
            {
                return BadRequest("No existe la computadora");
            }

            _context.Computadoras.Remove(computadoraEliminar);
            await _context.SaveChangesAsync();
            return Ok("Computadora eliminada exitosamente");
        }

        // Método GET BuscarComputadora que busca computadoras por cualquier campo
        [HttpGet]
        [Route("BuscarComputadora")]
        public async Task<IActionResult> Buscar([FromQuery] string query)
        {
            var resultados = await _context.Computadoras
                .Where(c => c.Nombre.Contains(query) || c.Descripcion.Contains(query) ||
                            c.Precio.ToString().Contains(query) || c.Categoria.ToString().Contains(query))
                .ToListAsync();

            if (resultados.Count == 0)
            {
                return NotFound("No se encontraron computadoras que coincidan con la búsqueda");
            }

            return Ok(resultados);
        }

        // Método GET FiltrarPorCategoria que filtra computadoras por categoría
        [HttpGet]
        [Route("FiltrarPorCategoria/{categoria:int}")]
        public async Task<IActionResult> FiltrarPorCategoria(int categoria)
        {
            var resultados = await _context.Computadoras
                .Where(c => c.Categoria == categoria)
                .ToListAsync();

            if (resultados.Count == 0)
            {
                return NotFound("No se encontraron computadoras en esta categoría");
            }

            return Ok(resultados);
        }

        // Método GET ListaComputadoras que devuelve la lista de todas las computadoras en la BD
        /*[HttpGet]
        [Route("ListaComputadoras")]
        public async Task<IActionResult> Lista()
        {
            var listaComputadoras = await _context.Computadoras.ToListAsync();
            return Ok(listaComputadoras);
        }*/
    }
}
