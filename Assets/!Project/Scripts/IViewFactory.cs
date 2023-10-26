public interface IViewFactory<TModel, TView>
{
    TView CreateViews(TModel model);
}