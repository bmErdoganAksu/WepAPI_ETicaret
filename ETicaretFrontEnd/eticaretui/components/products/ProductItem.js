import Link from "next/link";

export default function ProductItem({ product }) {
    return (
        <div className="card mb-4 p-2">
            <img src={product.productImage} className="card-img-top img-fluid w-25 m-auto" alt="..." />
            <div className="card-body">
                <h5 className="card-title">{product.modelName}</h5>
                <p className="card-text">{product.caption}</p>
                <div className="d-flex border justify-content-between rounded p-3">
                    {product.priceByStore.slice(0, 3).map(price => {
                        return (
                            <a target="_blank" href={price.link
                                .replaceAll("%3A", ":")
                                .replaceAll("%3F", "?")
                                .replaceAll("%3D", "=")
                                .replaceAll("%2F", "/")} key={price.id} className="text-center">
                                <div>
                                    <img className="d-block" src={price.storeName} />
                                    {price.price} ₺
                                </div>
                            </a>
                        )
                    })}
                </div>
                <Link href={`/product/${product.id}`} passHref>
                    <button className="btn btn-primary w-100">İncele</button>
                </Link>
            </div>
        </div>

    )
}