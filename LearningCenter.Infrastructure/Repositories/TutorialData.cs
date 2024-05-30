using _3_Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace _3_Data;

public class TutorialData : ITutorialData
{
    private readonly LearningCenterContext _learningCenterContext;

    public TutorialData(LearningCenterContext learningCenterContext)
    {
        _learningCenterContext = learningCenterContext;
    }

    public async Task<List<Tutorial>> GetAllAsync()
    {
        // COnecta BBDD MySQL
        /* List<Tutorial> data = new List<Tutorial>();
         data.Add(new Tutorial(){ Name = "tutorial 1"});
         data.Add(new Tutorial(){ Name = "tutorial 2"});
         data.Add(new Tutorial(){ Name = "tutorial 3"});*/

        var result = await _learningCenterContext.Tutorials.Where(t => t.IsActive)
            .Include(t => t.Sections).ToListAsync();

        return result;
    }

    public async Task<List<Tutorial>> GetSearchAsync(string name, int? year)
    {
        //var query = await _learningCenterContext.Tutorials.ToListAsync(); // 1M registr
        //var result = query.Where(t => t.IsActive && t.Name.Contains(name) && t.Year >= year).ToList();

        var result = await _learningCenterContext.Tutorials
            .Where(t => t.IsActive && t.Name.Contains(name) && t.Year >= year)
            .Include(t => t.Sections).ToListAsync(); //1000 Reigtros

        return result;
    }

    public Tutorial GetById(int id)
    {
        return _learningCenterContext.Tutorials.Where(t => t.Id == id && t.IsActive)
            .Include(t => t.Sections)
            .FirstOrDefault();
    }

    public async Task<Tutorial> GetByNameAsync(string name)
    {
        return await _learningCenterContext.Tutorials.Where(t => t.Name == name && t.IsActive).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Tutorial data)
    {
        using (var transaction = await _learningCenterContext.Database.BeginTransactionAsync())
        {
            try
            {
                data.IsActive = true;
                _learningCenterContext.Tutorials.Add(data); //no se refleja en BBDD
                await _learningCenterContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }


        return data.Id;
    }

    public bool Update(Tutorial data, int id)
    {
        var exitingTutorial = _learningCenterContext.Tutorials.Where(t => t.Id == id).FirstOrDefault();
        exitingTutorial.Name = data.Name;
        exitingTutorial.Description = data.Description;

        _learningCenterContext.Tutorials.Update(exitingTutorial);

        _learningCenterContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var exitingTutorial = _learningCenterContext.Tutorials.Where(t => t.Id == id).FirstOrDefault();

        // _learningCenterContext.Tutorials.Remove(exitingTutorial);
        exitingTutorial.IsActive = false;

        _learningCenterContext.Tutorials.Update(exitingTutorial);
        _learningCenterContext.SaveChanges();
        return true;
    }
}