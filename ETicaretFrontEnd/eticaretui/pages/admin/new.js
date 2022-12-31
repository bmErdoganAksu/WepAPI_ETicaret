import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import toast from "react-hot-toast";

export default function New() {
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const [prices, setPrices] = useState([]);
    const keys = [
        "brand",
        "modelName",
        "modelNo",
        "caption",
        "os",
        "processorType",
        "processorGeneration",
        "ram",
        "discSpace",
        "discType",
        "screenSize",
        "point",
        "priceByStore",
        "scrapeLink",
        "productImage"
    ]
    const onSubmit = async data => {
        console.log(data);
        try {
            data.priceByStore = prices;
            const response = await axios.post("https://localhost:7007/api/Computers/Add", data);
            toast.success(response?.data?.message)
        } catch (error) {
            toast.error(error?.data?.data?.message);
        }
    };
    return (
        <div className="container my-5">
            <form onSubmit={handleSubmit(onSubmit)}>
                {/* register your input into the hook by invoking the "register" function */}
                {keys.map((p, i) => <div key={i}><label className="form-label">{p}</label> <input className="form-control" {...register(p)} /></div>)}

                {/* {keys.map(price => <div key={price.id} className="card d-inline-block">
                <div className="card-header"><img src={price.storeName} /></div>
                <div className="card-body" ><input id="newPrice" defaultValue={price.price} /></div>
                <div className="card-footer">
                    <button onClick={(e) => handlePriceChange(e, price, document.querySelector("#newPrice").value)} className="btn btn-sm w-50 btn-success">Güncelle</button>
                    <button onClick={(e) => handleDelete(e, price)} className="btn btn-sm w-50 btn-danger">Sil</button>
                </div>
            </div>)} */}
                <button className="btn btn-outline-success mt-2 w-100" type="submit" >Kaydet</button>
            </form>
        </div>

    );
}