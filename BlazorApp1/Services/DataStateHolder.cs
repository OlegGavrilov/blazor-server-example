namespace BlazorApp1.Pages
{
    /// <summary>
    /// Container class to store loadable data
    /// </summary>
    public class DataStateHolder<T>
    {
        public bool IsLoading { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Value { get; set; }

        public async Task LoadDataAsync(Func<Task<T>> loadingFunc)
        {
            try
            {
                ErrorMessage = null;
                IsLoading = true;
                Value = await loadingFunc();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}