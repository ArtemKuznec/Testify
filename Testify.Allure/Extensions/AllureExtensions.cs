using Allure.Net.Commons;
using System;

namespace Testify.Allure.Extensions
{
    internal static class AllureExtensions
    {
        internal static T StartStep<T>(string name, Func<T> action)
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
                stepResult.name = name;
                AllureLifecycle.Instance.StopStep();
            }
        }

        internal static void StartStep(string name, Action action)
        {
            var stepResult = new StepResult();

            AllureLifecycle.Instance.StartStep(stepResult);

            try
            {
                action.Invoke();
                AllureLifecycle.Instance.UpdateStep(step => step.status = Status.passed);
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
                stepResult.name = name;
                AllureLifecycle.Instance.StopStep();
            }
        }
    }
}
