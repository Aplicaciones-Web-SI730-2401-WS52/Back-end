namespace _3_Data;

public interface ITutorialData
{
    Task<List<Tutorial>> GetAllAsync();
    Task<List<Tutorial>> GetSearchAsync(string name, int? year);

    Tutorial GetById(int id);

    Task<Tutorial> GetByNameAsync(string name);

    Task<int> SaveAsync(Tutorial data);

    bool Update(Tutorial data, int id);

    bool Delete(int id);
}