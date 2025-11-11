using Microsoft.AspNetCore.Mvc;
using TodoItemApi.Data;

namespace TodoItemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }
                // GET: api/<TodoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Todos.ToList());
        }

        //// GET api/<TodoController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var todo = list.Find(x => x.Id==id);
        //    if (todo == null)
        //    {
        //        //return NotFound();
        //        return NotFound(new { Message = $"Tarefa com Id={id} não encontrada." });
        //    }
        //    return Ok(todo);
        //}

       // POST api/todo
        [HttpPost]
        public IActionResult Post([FromBody] Todo newTodo)
        {
            if (newTodo == null)
                return BadRequest("O corpo da requisição é inválido.");

            if (_context.Todos.Any(x => x.Id == newTodo.Id))
                return Conflict(new { Message = $"Já existe uma tarefa com Id={newTodo.Id}." });

            _context.Todos.Add(newTodo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
            
        }

        // PUT api/todo/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Todo updatedTodo)
        //{
        //    if (updatedTodo == null)
        //        return BadRequest("O corpo da requisição é inválido.");

        //    var existing = list.FirstOrDefault(x => x.Id == id);
        //    if (existing == null)
        //        return NotFound(new { Message = $"Tarefa com Id={id} não encontrada." });

        //    // Atualiza o objeto existente (mantém o Id)
        //    existing.Name = updatedTodo.Name;
        //    existing.IsComplete = updatedTodo.IsComplete;

        //    return Ok(new
        //    {
        //        Message = "Tarefa atualizada com sucesso.",
        //        Updated = existing
        //    });
        //}

        //// PATCH api/todo/5/complete
        //[HttpPatch("{id}/complete")]
        //public IActionResult PatchMarkAsComplete(int id)
        //{
        //    var todo = list.FirstOrDefault(x => x.Id == id);
        //    if (todo == null)
        //        return NotFound(new { Message = $"Tarefa com Id={id} não encontrada." });

        //    todo.IsComplete = true;

        //    return Ok(new
        //    {
        //        Message = $"Tarefa '{todo.Name}' marcada como concluída.",
        //        Updated = todo
        //    });
        //}

        //// DELETE api/todo/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var todo = list.FirstOrDefault(x => x.Id == id);
        //    if (todo == null)
        //        return NotFound(new { Message = $"Tarefa com Id={id} não encontrada." });

        //    list.Remove(todo);
        //    return Ok(new { Message = $"Tarefa '{todo.Name}' removida com sucesso." });
        //}
    }
}
