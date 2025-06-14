using AutoMapper;
using WebApplication1.Entities;
using WebApplication1.Helpers;
using WebApplication1.Models.Professions;

namespace WebApplication1.Services;

public interface IProfessionService
{
    IEnumerable<Profession> GetAllProfessions();
    Profession GetProfessionById(int id);
    Profession CreateProfession(CreateProfessionRequest model);
    Profession UpdateProfession(int id, UpdateProfessionRequest model);
    void DeleteProfession(int id);
    Profession? GetProfessionByCode(string code);
}

public class ProfessionService : IProfessionService 
{
    private DataContext _context;
    private readonly IMapper _mapper;
    
    public ProfessionService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IEnumerable<Profession> GetAllProfessions()
    {
        return _context.Professions.ToList();
    }
    
    public Profession GetProfessionById(int id)
    {
        return getProfession(id);
    }
    
    public Profession GetProfessionByCode(string code)
    {
        var profession = _context.Professions.FirstOrDefault(p => p.Code == code);
        if (profession == null) throw new KeyNotFoundException("Profession not found");
        return profession;
    }
    
    public Profession CreateProfession(CreateProfessionRequest model)
    {
        
        // map model to new profession entity
        var profession = _mapper.Map<Profession>(model);
        
        // save profession
        _context.Professions.Add(profession);
        _context.SaveChanges();
        
        return profession;
    }
    
    public Profession UpdateProfession(int id, UpdateProfessionRequest model)
    {
        var profession = getProfession(id);
        
        // validate if needed
        
        // copy model properties to profession entity
        _mapper.Map(model, profession);
        
        // save changes
        _context.Professions.Update(profession);
        _context.SaveChanges();
        
        return profession;
    }
    
    public void DeleteProfession(int id)
    {
        var profession = getProfession(id);
        _context.Professions.Remove(profession);
        _context.SaveChanges();
    }
    
    // helper methods
    private Profession getProfession(int id)
    {
        var profession = _context.Professions.Find(id);
        if (profession == null) throw new KeyNotFoundException("Profession not found");
        return profession;
    }
}

