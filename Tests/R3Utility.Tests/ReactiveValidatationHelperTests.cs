﻿using System.ComponentModel.DataAnnotations;

namespace R3Utility.Tests;
public class ReactiveValidationHelperTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public void CombineLatestValuesAreAllFalse_ShouldEmitTrue_WhenAllSourcesAreFalse()
    {
        var subject1 = new BehaviorSubject<bool>(false);
        var subject2 = new BehaviorSubject<bool>(false);
        var combined = new[] { subject1, subject2 }.CombineLatestValuesAreAllFalse();

        bool result = false;
        var d = combined.Subscribe(value => result = value);

        result.ShouldBeTrue("because all source values are false");

        // Change one value to true and check
        subject1.OnNext(true);
        result.ShouldBeFalse("because one of the source values is true");

        subject1.Dispose();
        subject2.Dispose();
        d.Dispose();
    }

    [Fact]
    public void CombineLatestValuesAreAllTrue_ShouldEmitTrue_WhenAllSourcesAreTrue()
    {
        var subject1 = new BehaviorSubject<bool>(true);
        var subject2 = new BehaviorSubject<bool>(true);
        var combined = new[] { subject1, subject2 }.CombineLatestValuesAreAllTrue();

        bool result = false;
        var d = combined.Subscribe(value => result = value);

        result.ShouldBeTrue("because all source values are true");

        // Change one value to false and check
        subject2.OnNext(false);
        result.ShouldBeFalse("because one of the source values is false");

        subject1.Dispose();
        subject2.Dispose();
        d.Dispose();
    }

    [Fact]
    public void CombineLatestValuesAreAnyFalse_ShouldEmitTrue_WhenAnySourceIsFalse()
    {
        var subject1 = new BehaviorSubject<bool>(true);
        var subject2 = new BehaviorSubject<bool>(false);
        var combined = new[] { subject1, subject2 }.CombineLatestValuesAreAnyFalse();

        bool result = false;
        var d = combined.Subscribe(value => result = value);
        result.ShouldBeTrue("because one of the source values is false");

        // Change both values to true and check
        subject2.OnNext(true);
        result.ShouldBeFalse("because all source values are now true");

        subject1.Dispose();
        subject2.Dispose();
        d.Dispose();
    }

    [Fact]
    public void CombineLatestValuesAreAnyTrue_ShouldEmitTrue_WhenAnySourceIsTrue()
    {
        var subject1 = new BehaviorSubject<bool>(false);
        var subject2 = new BehaviorSubject<bool>(true);
        var combined = new[] { subject1, subject2 }.CombineLatestValuesAreAnyTrue();

        bool result = false;
        var d = combined.Subscribe(value => result = value);

        result.ShouldBeTrue("because one of the source values is true");

        // Change both values to false and check
        subject2.OnNext(false);
        result.ShouldBeFalse("because all source values are now false");

        subject1.Dispose();
        subject2.Dispose();
        d.Dispose();
    }

    [Range(0, 10)]
    public BindableReactiveProperty<int> IntBRP { get; set; } = new BindableReactiveProperty<int>().EnableValidation<ReactiveValidationHelperTests>();
    [Required]
    public BindableReactiveProperty<string> StringBRP { get; set; } = new BindableReactiveProperty<string>("").EnableValidation<ReactiveValidationHelperTests>();

    [Fact]
    public void CreateCanExecuteSource_ShouldReturnFalse_WhenAnyPropertyHasErrors()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(IntBRP, StringBRP);

        bool canExecute = true;
        var d = source.Subscribe(value => canExecute = value);

        IntBRP.Value = 5;
        StringBRP.Value = "aaa";

        fixture.FrameProvider.Advance();
        canExecute.ShouldBeTrue();

        IntBRP.Value = 11;
        fixture.FrameProvider.Advance();
        canExecute.ShouldBeFalse();

        d.Dispose();
    }

    [Fact]
    public void CreateCanExecuteSource_ShouldReturnFalse_WhenAllPropertyHasErrors()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(IntBRP, StringBRP);
        bool canExecute = true;
        var d = source.Subscribe(value => canExecute = value);

        IntBRP.Value = 11;
        StringBRP.Value = "";

        fixture.FrameProvider.Advance();
        canExecute.ShouldBeFalse();

        d.Dispose();
    }

    [Fact]
    public void CreateCanExecuteSource_ShouldReturnTrue_WhenAllPropertiesHaveNoErrors()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(IntBRP, StringBRP);

        bool canExecute = false;
        var d = source.Subscribe(value => canExecute = value);

        IntBRP.Value = 10;
        StringBRP.Value = "aaa";

        fixture.FrameProvider.Advance();
        canExecute.ShouldBeTrue();
        d.Dispose();
    }

    [Fact]
    public void CreateCanExecuteSource_ShouldThrow_ArgumentNullException_WhenPropertiesAreNull()
    {
        Should.Throw<ArgumentNullException>(() => ReactiveValidationHelper.CreateCanExecuteSource(null!));
    }

    [Fact]
    public void CreateCanExecuteSource_ShouldThrow_ArgumentNullException_WhenPropertiesAreEmpty()
    {
        Should.Throw<ArgumentNullException>(() => ReactiveValidationHelper.CreateCanExecuteSource());
    }

    [Fact]
    public void CreateCanExecuteSource_ShouldThrow_WhenPropertyValidationNotEnabled()
    {
        var propertyWithoutValidation = new BindableReactiveProperty<int>();

        var exception = Should.Throw<ArgumentException>(() =>
        {
            var source = ReactiveValidationHelper.CreateCanExecuteSource(StringBRP, IntBRP, propertyWithoutValidation);
            source.ToReactiveCommand();
        });

        exception.Message.ShouldContain("EnableValidation()");
    }

    [Fact]
    public void CreateCanExecuteSource_WithForceValidationOnStart_ShouldReturnFalseInitially()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(true, StringBRP);
        bool canExecute = true;
        
        var d = source.Subscribe(value => canExecute = value);
        
        fixture.FrameProvider.Advance();
        canExecute.ShouldBeFalse();
        d.Dispose();
    }

    [Fact]
    public void CreateCanExecuteSource_WithoutForceValidationOnStart_ShouldReturnTrueInitially()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(false, StringBRP);
        bool canExecute = false;

        var d = source.Subscribe(value => canExecute = value);

        fixture.FrameProvider.Advance();
        canExecute.ShouldBeTrue();
        d.Dispose();
    }
}
