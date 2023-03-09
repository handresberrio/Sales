using System.ComponentModel.DataAnnotations;


namespace Sales.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estado/Departamento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres.")]

        public string Name { get; set; } = null!;

        public int CountryId { get; set; } // para el boton regresar saber en que pais estaba

        public Country? Country { get; set; } // relacion de uno a muchos

        public ICollection<City>? Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumber  => Cities == null ? 0 : Cities.Count;
    }
}
