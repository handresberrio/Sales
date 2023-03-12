using Microsoft.EntityFrameworkCore;
using Sales.API.Helpers;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Enums;
using Sales.Shared.ResponsesApi;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);

        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task CheckCountriesAsync()
        {
            //if (!_context.Countries.Any())
            //{
                ResponseGeneric responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<CountryResponse> countries = (List<CountryResponse>)responseCountries.Result!;
                    foreach (CountryResponse countryResponse in countries)
                    {
                        Country country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            ResponseGeneric responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        ResponseGeneric responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            //}
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "camisas" });
                _context.Categories.Add(new Category { Name = "blusas" });
                _context.Categories.Add(new Category { Name = "suéteres" });
                _context.Categories.Add(new Category { Name = "pantalones" });
                _context.Categories.Add(new Category { Name = "faldas" });
                _context.Categories.Add(new Category { Name = "casacas" });
                _context.Categories.Add(new Category { Name = "abrigos" });
                _context.Categories.Add(new Category { Name = "trajes" });
                _context.Categories.Add(new Category { Name = "medias" });
                _context.Categories.Add(new Category { Name = "ropa interior" });
                _context.Categories.Add(new Category { Name = "bolsos de mano" });
                _context.Categories.Add(new Category { Name = "bufandas" });
                _context.Categories.Add(new Category { Name = "cinturones" });
                _context.Categories.Add(new Category { Name = "sombreros" });
                _context.Categories.Add(new Category { Name = "trajes de baño" });
                _context.Categories.Add(new Category { Name = "uniformes" });
                _context.Categories.Add(new Category { Name = "gorras" });
                _context.Categories.Add(new Category { Name = "sudaderas" });
                _context.Categories.Add(new Category { Name = "zapatos" });
                _context.Categories.Add(new Category { Name = "camisillas" });
                _context.Categories.Add(new Category { Name = "busos" });
                _context.Categories.Add(new Category { Name = "guantes" });
                _context.Categories.Add(new Category { Name = "chanclas" });
                _context.Categories.Add(new Category { Name = "bolsos" });
                _context.Categories.Add(new Category { Name = "morrales" });
                _context.Categories.Add(new Category { Name = "vestidos" });
                _context.Categories.Add(new Category { Name = "1camisas" });
                _context.Categories.Add(new Category { Name = "1blusas" });
                _context.Categories.Add(new Category { Name = "1suéteres" });
                _context.Categories.Add(new Category { Name = "1pantalones" });
                _context.Categories.Add(new Category { Name = "1faldas" });
                _context.Categories.Add(new Category { Name = "1casacas" });
                _context.Categories.Add(new Category { Name = "1abrigos" });
                _context.Categories.Add(new Category { Name = "1trajes" });
                _context.Categories.Add(new Category { Name = "1medias" });
                _context.Categories.Add(new Category { Name = "1ropa interior" });
                _context.Categories.Add(new Category { Name = "1bolsos de mano" });
                _context.Categories.Add(new Category { Name = "1bufandas" });
                _context.Categories.Add(new Category { Name = "1cinturones" });
                _context.Categories.Add(new Category { Name = "1sombreros" });
                _context.Categories.Add(new Category { Name = "1trajes de baño" });
                _context.Categories.Add(new Category { Name = "1uniformes" });
                _context.Categories.Add(new Category { Name = "1gorras" });
                _context.Categories.Add(new Category { Name = "1sudaderas" });
                _context.Categories.Add(new Category { Name = "1zapatos" });
                _context.Categories.Add(new Category { Name = "1camisillas" });
                _context.Categories.Add(new Category { Name = "1busos" });
                _context.Categories.Add(new Category { Name = "1guantes" });
                _context.Categories.Add(new Category { Name = "1chanclas" });
                _context.Categories.Add(new Category { Name = "1bolsos" });
                _context.Categories.Add(new Category { Name = "1morrales" });
                _context.Categories.Add(new Category { Name = "1vestidos" });
                _context.Categories.Add(new Category { Name = "2camisas" });
                _context.Categories.Add(new Category { Name = "2blusas" });
                _context.Categories.Add(new Category { Name = "2suéteres" });
                _context.Categories.Add(new Category { Name = "2pantalones" });
                _context.Categories.Add(new Category { Name = "2faldas" });
                _context.Categories.Add(new Category { Name = "2casacas" });
                _context.Categories.Add(new Category { Name = "2abrigos" });
                _context.Categories.Add(new Category { Name = "2trajes" });
                _context.Categories.Add(new Category { Name = "2medias" });
                _context.Categories.Add(new Category { Name = "2ropa interior" });
                _context.Categories.Add(new Category { Name = "2bolsos de mano" });
                _context.Categories.Add(new Category { Name = "2bufandas" });
                _context.Categories.Add(new Category { Name = "2cinturones" });
                _context.Categories.Add(new Category { Name = "2sombreros" });
                _context.Categories.Add(new Category { Name = "2trajes de baño" });
                _context.Categories.Add(new Category { Name = "2uniformes" });
                _context.Categories.Add(new Category { Name = "2gorras" });
                _context.Categories.Add(new Category { Name = "2sudaderas" });
                _context.Categories.Add(new Category { Name = "2zapatos" });
                _context.Categories.Add(new Category { Name = "2camisillas" });
                _context.Categories.Add(new Category { Name = "2busos" });
                _context.Categories.Add(new Category { Name = "2guantes" });
                _context.Categories.Add(new Category { Name = "2chanclas" });
                _context.Categories.Add(new Category { Name = "2bolsos" });
                _context.Categories.Add(new Category { Name = "2morrales" });
                _context.Categories.Add(new Category { Name = "2vestidos" });
                _context.Categories.Add(new Category { Name = "3camisas" });
                _context.Categories.Add(new Category { Name = "3blusas" });
                _context.Categories.Add(new Category { Name = "3suéteres" });
                _context.Categories.Add(new Category { Name = "3pantalones" });
                _context.Categories.Add(new Category { Name = "3faldas" });
                _context.Categories.Add(new Category { Name = "3casacas" });
                _context.Categories.Add(new Category { Name = "3abrigos" });
                _context.Categories.Add(new Category { Name = "3trajes" });
                _context.Categories.Add(new Category { Name = "3medias" });
                _context.Categories.Add(new Category { Name = "3ropa interior" });
                _context.Categories.Add(new Category { Name = "3bolsos de mano" });
                _context.Categories.Add(new Category { Name = "3bufandas" });
                _context.Categories.Add(new Category { Name = "3cinturones" });
                _context.Categories.Add(new Category { Name = "3sombreros" });
                _context.Categories.Add(new Category { Name = "3trajes de baño" });
                _context.Categories.Add(new Category { Name = "3uniformes" });
                _context.Categories.Add(new Category { Name = "3gorras" });
                _context.Categories.Add(new Category { Name = "3sudaderas" });
                _context.Categories.Add(new Category { Name = "3zapatos" });
                _context.Categories.Add(new Category { Name = "3camisillas" });
                _context.Categories.Add(new Category { Name = "3busos" });
                _context.Categories.Add(new Category { Name = "3guantes" });
                _context.Categories.Add(new Category { Name = "3chanclas" });
                _context.Categories.Add(new Category { Name = "3bolsos" });
                _context.Categories.Add(new Category { Name = "3morrales" });
                _context.Categories.Add(new Category { Name = "3vestidos" });
            }

            await _context.SaveChangesAsync();
        }

    }
}
