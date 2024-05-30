using _3_Data;
using _3_Shared;


namespace _2_Domain;

public class TutorialDomain : ITutorialDomain
{
    private readonly ITutorialData _tutorialData;

    public TutorialDomain(ITutorialData tutorialData)
    {
        _tutorialData = tutorialData;
    }

    public async Task<int> SaveAsync(Tutorial data)
    {
        var existingTutorial = await _tutorialData.GetByNameAsync(data.Name);
        if (existingTutorial != null) throw new Exception("Tutorial already exists");

        var total = (await _tutorialData.GetAllAsync()).Count();
        if (total >= Constans.MAX_TUTORIALS) throw new Exception("Max tutorials reached " + Constans.MAX_TUTORIALS);

        if (data.Sections.Count() < Constans.MIN_SECCTIONS)
            throw new Exception("Min sections required " + Constans.MIN_SECCTIONS);

        return await _tutorialData.SaveAsync(data);
    }

    public bool Update(Tutorial data, int id)
    {
        //Bussine rules
        var existingTutorial = _tutorialData.GetById(id);

        if (existingTutorial == null) throw new Exception("Tutorial not found");

        if (existingTutorial.Description != data.Description)
            throw new Exception("Description can not be updated");

        return _tutorialData.Update(data, id);
    }

    public  bool Delete(int id)
    {
        //Bussine rules
        var existingTutorial = _tutorialData.GetById(id);
        if (existingTutorial == null) throw new Exception("Tutorial not found");
        return  _tutorialData.Delete(id);
    }
}