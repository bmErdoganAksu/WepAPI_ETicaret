import axios from "axios";
import { useRouter } from "next/router"
import { useCallback, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import toast from "react-hot-toast";

export default function Update() {
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const onSubmit = async data => {
        try {
            data.priceByStore = [];
            const response = await axios.post("https://localhost:7007/api/Computers/Update", data);
            toast.success(response?.data?.message)
        } catch (error) {
            toast.error(error?.data?.data?.message);
        }
    };
    const router = useRouter();
    const { id } = router.query;
    const [product, setProduct] = useState(null);
    const getProduct = useCallback(async () => {
        try {
            if (!router.isReady) return;
            const response = await axios.get("https://localhost:7007/api/Computers/GetById/" + id)
            setProduct(response.data.data)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }, [id])
    const handlePriceChange = async (e, priceByStore, newPrice) => {
        e.preventDefault();
        priceByStore.price = newPrice;
        try {
            if (!router.isReady) return;
            const response = await axios.post("https://localhost:7007/api/Computers/UpdatePrice", priceByStore)
            toast.success(response?.data?.message)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
        console.log(priceByStore, newPrice);
    }
    const handleDelete = async (e, priceByStore) => {
        console.log(priceByStore);
        e.preventDefault();
        try {
            if (!router.isReady) return;
            const response = await axios.post("https://localhost:7007/api/Computers/DeletePrice/" + priceByStore.id)
            toast.success(response?.data?.message)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }
    useEffect(() => {
        getProduct();
    }, [id, getProduct])
    return (
        <div className="container py-5">

            <form onSubmit={handleSubmit(onSubmit)}>
                {/* register your input into the hook by invoking the "register" function */}
                {product && Object.entries(product).map((p, i) => <div key={i}><label className="form-label">{p[0] == "priceByStore" ? "" : p[0]}</label> <input type={p[0] == "priceByStore" ? "hidden" : "text"} readOnly={p[0] == "id"} className="form-control" defaultValue={p[0] != "priceByStore" ? p[1] : ""} {...register(p[0])} /></div>)}

                {product && product.priceByStore.map(price => <div key={price.id} className="card d-inline-block">
                    <div className="card-header"><img src={price.storeName} /></div>
                    <div className="card-body" ><input id="newPrice" defaultValue={price.price} /></div>
                    <div className="card-footer">
                        <button onClick={(e) => handlePriceChange(e, price, document.querySelector("#newPrice").value)} className="btn btn-sm w-50 btn-success">Güncelle</button>
                        <button onClick={(e) => handleDelete(e, price)} className="btn btn-sm w-50 btn-danger">Sil</button>
                    </div>
                </div>)}
                <button className="btn btn-outline-success mt-2 w-100" type="submit" >Kaydet</button>
            </form>
        </div >

    )
}