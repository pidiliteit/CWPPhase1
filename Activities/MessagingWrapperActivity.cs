using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace TeckraftBuilds.Workflow.Activities
{
    public abstract class MessagingWrapperActivity
    {
        protected Variable<T> AddVariable<T>(ModelItemCollection variables, Variable<T> variable)
        {
            // Get variable.
            ModelItem modelItem = variables.FirstOrDefault(
                m => ((Variable)m.GetCurrentValue()).Name == variable.Name);

            // Add into variables collection if it does not exist.
            if (modelItem == null)
                variables.Add(variable);

            // Assign it if it was found.
            else
                variable = modelItem.GetCurrentValue() as Variable<T>;

            return variable;
        }

        protected ModelItemCollection GetVariableCollection(DependencyObject target)
        {
            ModelItemCollection variables = null;

            // Get model service.
            WorkflowViewElement element = target as WorkflowViewElement;
            ModelService modelService = element.Context.Services.GetService<ModelService>();

            // Locate the top-most sequence.
            var list = modelService.Find(modelService.Root, typeof(Activity));
            ModelItem container = null;

            // Get the container item. If there is no top-most container, just return the
            // the immediate Sequence activity.
            container = (list.Count() > 0) ? list.First<ModelItem>() : element.ModelItem;

            // Get the variable collection.
            if (container.Properties["Variables"] != null)
                variables = container.Properties["Variables"].Collection;

            return variables;
        }
    }
}
