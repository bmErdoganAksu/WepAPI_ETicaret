import { useRouter } from "next/router";
import axios from "axios";

import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";

export default function Product() {
    const router = useRouter();
    const { productId } = router.query;
    const [product, setProduct] = useState(null);
    const getProduct = useCallback(async () => {
        try {
            if (!router.isReady) return;
            const response = await axios.get("https://localhost:7007/api/Computers/GetById/" + productId)
            setProduct(response.data.data)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }, [productId])
    useEffect(() => {
        getProduct();
    }, [productId, getProduct])

    return (
        <div className="container my-5">
            <div className="row">
                <div className="col-3">
                    <img src={product?.productImage} />
                    <h4>{product?.modelName}</h4>
                </div>
                <div className="col-9">
                    <strong className="d-block">{product?.caption}</strong>
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">Özellik</th>
                                <th scope="col">Açıklama</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">Marka</th>
                                <td>{product?.brand}</td>
                            </tr>
                            <tr>
                                <th scope="row">Disk Boyutu</th>
                                <td>{product?.discSpace}</td>
                            </tr>
                            <tr>
                                <th scope="row">Disk Tipi</th>
                                <td>{product?.discType}</td>
                            </tr>
                            <tr>
                                <th scope="row">Model No</th>
                                <td>{product?.modelNo}</td>
                            </tr>
                            <tr>
                                <th scope="row">İşletim Sistemi</th>
                                <td>{product?.os}</td>
                            </tr>
                            <tr>
                                <th scope="row">İşlemci Jenerasyonu</th>
                                <td>{product?.processorGeneration}</td>
                            </tr>
                            <tr>
                                <th scope="row">İşlemci Tipi</th>
                                <td>{product?.processorType}</td>
                            </tr>
                            <tr>
                                <th scope="row">Ram</th>
                                <td>{product?.ram}</td>
                            </tr>
                            <tr>
                                <th scope="row">Ekran Boyutu</th>
                                <td>{product?.screenSize}</td>
                            </tr>
                        </tbody>
                    </table>
                    {product?.priceByStore?.map(price => {
                        return (
                            <a target="_blank" href={price.link
                                .replaceAll("%3A", ":")
                                .replaceAll("%3F", "?")
                                .replaceAll("%3D", "=")
                                .replaceAll("%2F", "/")} className="d-inline-block me-2 justify-content-between" key={price.id}>
                                <div className="text-center">
                                    <img className="d-block" src={price.storeName} />
                                    <span>{price.price} ₺</span>
                                </div>
                            </a>
                        )
                    })}
                </div>
            </div>

        </div>
    )
}