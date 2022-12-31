
import ProductItem from "./ProductItem";

export default function ProductList({ products }) {

    return (
        <>
            {products && products.map((product, index) => <ProductItem key={index} product={product} />)}
        </>
    )
}