namespace Prj.Services.Models
{
    /// <summary>
    /// مشترکاً بین کلاس هایی که تنها یک کلید و یک ولیو میگیرند
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TTitle"></typeparam>
    public class DefaultServiceModel<TKey, TTitle>
    {
        public TKey Id { get; set; }

        public TTitle Title { get; set; }
    }

    public class DefaultWithImageServiceModel<TKey, TTitle>
    {
        public TKey Id { get; set; }

        public TTitle Title { get; set; }

        public string ImageUrl { get; set; }
    }

    public class DefaultServiceModel<TKey>
    {
        public TKey Id { get; set; }
    }

    /// <summary>
    /// مشترکاً بین کلاس هایی که تنها یک کلید و یک ولیو میگیرند
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DefaultKeyValueServiceModel<TKey, TValue>
    {
        public TKey Id { get; set; }

        public TValue Value { get; set; }
    }
}