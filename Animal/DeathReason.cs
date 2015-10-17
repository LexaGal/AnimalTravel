using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnimalLib
{
    public enum DeathReason
    {
        [Description("FOOD REACTION")] FoodReaction,
        [Description("ACTION REACTION")] ActionReaction,
        [Description("NO FOOD")] NoFood
    }
}