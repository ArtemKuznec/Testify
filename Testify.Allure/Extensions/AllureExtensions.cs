using Testify.Allure;
using System;
using Allure.Net.Commons;

namespace Testify.Allure.Extensions
{
    internal static class AllureExtensions
    {
        internal static T StartStep<T>(Func<string> name, Func<T> action)
        {
            var stepResult = new StepResult();

            AllureLifecycle.Instance.StartStep(stepResult);

            try
            {
                var value = action.Invoke();
                AllureLifecycle.Instance.UpdateStep(step => step.status = Status.passed);
                return value;
            }
            catch (Exception exception)
            {
                AllureLifecycle.Instance.UpdateStep(step =>
                {
                    step.status = Status.broken;
                    step.statusDetails = new StatusDetails
                    {
                        trace = exception.StackTrace,
                        message = exception.Message
                    };
                });

                throw;
            }
            finally
            {
                stepResult.name = name.Invoke();
                AllureLifecycle.Instance.StopStep();
            }
        }
    }
}
