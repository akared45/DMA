import { useState, useEffect } from 'react';
import { Table, Button, Alert, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function WardList() {
  const [wards, setWards] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    api.get('/wards').then(res => {
      setWards(res.data);
      setLoading(false);
    });
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm('Xóa khoa này?')) {
      await api.delete(`/wards/${id}`);
      setWards(wards.filter(w => w.wardId !== id));
    }
  };

  if (loading) return <Spinner animation="border" className="d-block mx-auto mt-5" />;

  return (
    <>
      <div className="d-flex justify-content-between mb-3">
        <h2>Danh sách Khoa</h2>
        <Button variant="success" onClick={() => navigate('/wards/new')}>Thêm Khoa</Button>
      </div>

      <Table striped bordered hover>
        <thead className="table-dark">
          <tr>
            <th>Tên khoa</th>
            <th>Sức chứa</th>
            <th>Số y tá</th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody>
          {wards.map(w => (
            <tr key={w.wardId}>
              <td>{w.name}</td>
              <td>{w.capacity}</td>
              <td>{w.nurses?.length || 0}</td>
              <td>
                <Button size="sm" variant="warning" className="me-2"
                  onClick={() => navigate(`/wards/edit/${w.wardId}`)}>Sửa</Button>
                <Button size="sm" variant="danger"
                  onClick={() => handleDelete(w.wardId)}>Xóa</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}