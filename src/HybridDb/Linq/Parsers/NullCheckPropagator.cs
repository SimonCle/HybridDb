using HybridDb.Linq.Ast;

namespace HybridDb.Linq.Parsers
{
    //TODO:
    //public class NullCheckPropagator : SqlExpressionVisitor
    //{
    //    protected override SqlExpression Visit(SqlBinaryExpression expression)
    //    {
    //        if (expression.Right.NodeType == SqlNodeType.Constant &&
    //            ((Constant) expression.Right).Value == null)
    //        {
    //            switch (expression.NodeType)
    //            {
    //                case SqlNodeType.Equal:
    //                    return new SqlBinaryExpression(SqlNodeType.Is, expression.Left, new Constant(typeof(object), null));
    //                case SqlNodeType.NotEqual:
    //                    return new SqlBinaryExpression(SqlNodeType.IsNot, expression.Left, new Constant(typeof(object), null));
    //            }
    //        }

    //        return base.Visit(expression);
    //    }
    //}
}