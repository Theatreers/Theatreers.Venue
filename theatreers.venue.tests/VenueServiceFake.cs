using System.Collections.Generic;
using System.Linq;
using theatreers.shared.Models;
using theatreers.shared.Interfaces;

public class VenueServiceFake : IVenueService
{
    private readonly List<Venue> _VenueList;
 
    public VenueServiceFake()
    {
        _VenueList = new List<Venue>()
            {
                new Venue() { Id = 1, Name = "Hexagon" },
                new Venue() { Id = 2, Name = "Luckley School" },
                new Venue() { Id = 3, Name = "A random church" }
            };
    }
 
    public IEnumerable<Venue> GetAll()
    {
        return _VenueList;
    }
    
    public Venue GetById(int id)
    {
        return _VenueList.Where(a => a.Id == id)
            .FirstOrDefault();
    }
 
    public Venue Create(Venue newItem)
    {
        newItem.Id = _VenueList.Count + 1;
        _VenueList.Add(newItem);
        return newItem;
    }

    public Venue Update(Venue body)
    {
        var existing = _VenueList.First(a => a.Id == body.Id);
        return existing;
    }
 
    public void Delete(int id)
    {
        var existing = _VenueList.First(a => a.Id == id);
        _VenueList.Remove(existing);
    }
}