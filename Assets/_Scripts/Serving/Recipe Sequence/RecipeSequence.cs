namespace Serving
{
    public interface RecipeSequence
    {
        void LoadRecipes();
        Recipe[] GetRecipes();
    }
}