using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Sales.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres.")]
        public string Name { get; set; } = null!;

        public ICollection<State>?  States { get; set; } // relacion muchos a uno

        [Display(Name = "Estados/Departamentos")]
        public int StatesNumber => States == null ? 0 : States.Count; // propiedad de lectura me dice cuantos estados tiene un pais
    }
}
