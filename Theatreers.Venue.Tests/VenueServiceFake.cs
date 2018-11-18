using System.Collections.Generic;
using System.Linq;
using Theatreers.Shared.Models;
using Theatreers.Shared.Interfaces;

public class VenueServiceFake : IVenueService
{
    private readonly List<VenueModel> _VenueList;
 
    public VenueServiceFake()
    {
        _VenueList = new List<VenueModel>()
            {
                new VenueModel() { Id = 1, Name = "Hexagon" },
                new VenueModel() { Id = 2, Name = "Luckley School" },
                new VenueModel() { Id = 3, Name = "A random church" }
            };
    }
 
    public IEnumerable<VenueModel> GetAll()
    {
        return _VenueList;
    }
    
    public VenueModel GetById(int id)
    {
        return _VenueList.Where(a => a.Id == id)
            .FirstOrDefault();
    }
 
    public VenueModel Create(VenueModel newItem)
    {
        newItem.Id = _VenueList.Count + 1;
        _VenueList.Add(newItem);
        return newItem;
    }

    public VenueModel Update(VenueModel body)
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