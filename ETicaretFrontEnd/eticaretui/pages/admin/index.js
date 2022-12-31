import Link from "next/link";

export default function Admin() {
    return (
        <div className="container mt-5">
            <div className="row">
                <div className="col-3">
                    <ul className="list-group">
                        <li className="list-group-item">
                            <Link href="/admin/products">Ürünler</Link>
                        </li>
                        <li className="list-group-item">
                            <Link href="/admin/new">Ürün Ekle</Link>
                        </li>
                    </ul>
                </div>
                <div className="col-9">Yazlab</div>
            </div>
        </div>
    )
}