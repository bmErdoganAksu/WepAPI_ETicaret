import axios from "axios";
import Link from "next/link";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";

export default function Products() {
    const [products, setProducts] = useState(null);

    const getProducts = useCallback(async () => {
        try {
            const response = await axios.get("https://localhost:7007/api/Computers/GetAll")
            setProducts(response.data.data)
            // .then(response => setProducts(response.data.data))
            // .catch(error => toast.error(error.response.data.message ?? error.message))
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }, [])
    useEffect(() => {
        getProducts()
    }, [getProducts])

    const handleDelete = async (id) => {
        try {
            const response = await axios.post("https://localhost:7007/api/Computers/DeleteComputer/" + id)
            toast.success(response?.data?.message)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }
    return (
        <div className="container mt-5">
            <div className="row">
                <div className="col-md-12 overflow-auto">
                    <table className="table-responsive table-bordered border-primary">
                        <thead>
                            <tr>
                                <th>İşlem</th>
                                {products && Object.keys(products[0]).map((k, i) => <th scope="col" key={i}>{k}</th>)}
                            </tr>
                        </thead>
                        <tbody>
                            {products && products.map(product => {
                                return (
                                    <tr key={product.id}>
                                        <td>
                                            <Link href={`/admin/update/${product.id}`} passHref>
                                                <button className="btn btn-sm btn-success">Güncelle</button>
                                            </Link>
                                            <button className="btn btn-sm btn-danger" onClick={() => handleDelete(product.id)}>Sil</button>
                                        </td>
                                        {products && Object.values(product).map((p, i) => <td key={i}>{p.toString()}</td>)}
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    )
}