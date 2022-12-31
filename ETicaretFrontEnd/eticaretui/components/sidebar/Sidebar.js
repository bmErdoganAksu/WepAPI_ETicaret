export default function Sidebar({ brands, setBrandFilter, brandFilter }) {
    return (
        <div className="card">
            <div className="card-header">Marka Filtresi</div>
            <div className="card-body">
                <ul className="list-group">
                    {brands && brands.map((brand, i) =>
                        <li key={i} className="list-group-item">
                            <input
                                onInput={(r => {
                                    if (r.target.checked)
                                        setBrandFilter([...brandFilter, r.target.value])
                                    else setBrandFilter([...brandFilter.filter(f => f !== r.target.value)])
                                })} className="form-check-input me-1" type="checkbox" value={brand} id={`_${i}`} />
                            <label className="form-check-label stretched-link" htmlFor={`_${i}`}>{brand}</label>
                        </li>
                    )}
                </ul>

            </div>
        </div>
    )
}