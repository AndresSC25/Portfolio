using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class DiscountManager
{
    public void Create(Discount discount)
    {
        var discountCrud = new DiscountCrudFactory();

        if (!IsValidType(discount)) throw new Exception("Tipo de descuento no válido.");

        if (!IsValidCoupon(discount)) throw new Exception("Cupón no válido.");

        if (!IsValidPercentage(discount)) throw new Exception("Porcentaje no válido.");

        if (!IsValidValidFrom(discount)) throw new Exception("Fecha de inicio no válida.");

        if (!IsValidValidTo(discount)) throw new Exception("Fecha de fin no válida.");

        discountCrud.Create(discount);
    }

    public void Update(Discount discount)
    {
        var discountCrud = new DiscountCrudFactory();

        if (!IsValidType(discount)) throw new Exception("Tipo de descuento no válido.");

        if (!IsValidCoupon(discount)) throw new Exception("Cupón no válido.");

        if (!IsValidPercentage(discount)) throw new Exception("Porcentaje no válido.");

        if (!IsValidValidFrom(discount)) throw new Exception("Fecha de inicio no válida.");

        if (!IsValidValidTo(discount)) throw new Exception("Fecha de fin no válida.");

        discountCrud.Update(discount);
    }

    public void Delete(Discount discount)
    {
        var discountCrud = new DiscountCrudFactory();
        if (!IsNotMatriculaOrInactive(discount))
            throw new Exception("No puede borrar el descuento de matricula o de inactividad.");
        discountCrud.Delete(discount);
    }

    public List<Discount> RetrieveAll()
    {
        var discountCrud = new DiscountCrudFactory();
        return discountCrud.RetrieveAll<Discount>();
    }

    public Discount RetrieveById(int id)
    {
        var discountCrud = new DiscountCrudFactory();
        return discountCrud.RetrieveById<Discount>(id);
    }

    #region Validations

    public bool IsValidType(Discount discount)
    {
        return !string.IsNullOrWhiteSpace(discount.Type) &&
               discount.Type.Length >= 2 &&
               discount.Type.Length <= 50;
    }

    public bool IsValidCoupon(Discount discount)
    {
        return !string.IsNullOrWhiteSpace(discount.Coupon) &&
               discount.Coupon.Length >= 2 &&
               discount.Coupon.Length <= 50;
    }

    public bool IsValidPercentage(Discount discount)
    {
        return discount.Percentage >= 0 && discount.Percentage <= 100;
    }

    public bool IsValidValidFrom(Discount discount)
    {
        // ValidFrom should not be in the past
        return discount.ValidFrom >= DateTime.Today;
    }

    public bool IsValidValidTo(Discount discount)
    {
        // ValidTo should be after ValidFrom
        return discount.ValidTo > discount.ValidFrom;
    }

    public bool IsNotMatriculaOrInactive(Discount discount)
    {
        // ValidTo should be after ValidFrom
        return discount.Id != 0 && discount.Id != 1;
    }

    public bool IsValidDiscount(Discount discount)
    {
        return IsValidType(discount) &&
               IsValidCoupon(discount) &&
               IsValidPercentage(discount) &&
               IsValidValidFrom(discount) &&
               IsValidValidTo(discount);
    }

    #endregion
}