import { useState, useEffect } from 'react';
import { Form, Button, Alert, Card } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function NurseForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(id);

  const [form, setForm] = useState({ name: '', certification: '', wardId: '' });
  const [wards, setWards] = useState([]);
  const [error, setError] = useState('');
  const [validated, setValidated] = useState(false);

  useEffect(() => {
    api.get('/wards').then(res => setWards(res.data));
    if (isEdit) {
      api.get(`/nurses/${id}`).then(res => setForm(res.data));
    }
  }, [id, isEdit]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formEl = e.currentTarget;
    if (formEl.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }

    try {
      if (isEdit) {
        await api.put(`/nurses/${id}`, { ...form, nurseId: id });
      } else {
        await api.post('/nurses', form);
      }
      navigate('/nurses');
    } catch (err) {
      setError(err.response?.data || 'Lỗi server');
    }
  };

  return (
    <Card className="mx-auto mt-4" style={{ maxWidth: '600px' }}>
      <Card.Header>
        <h4>{isEdit ? 'Sửa' : 'Thêm'} Y tá</h4>
      </Card.Header>
      <Card.Body>
        {error && <Alert variant="danger">{error}</Alert>}
        <Form noValidate validated={validated} onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Tên đầy đủ</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.name || ''}
              onChange={e => setForm({ ...form, name: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">
              Vui lòng nhập tên
            </Form.Control.Feedback>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Chứng chỉ</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.certification || ''}
              onChange={e => setForm({ ...form, certification: e.target.value })}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Khoa</Form.Label>
            <Form.Select
              required
              value={form.wardId || ''}
              onChange={e => setForm({ ...form, wardId: parseInt(e.target.value) })}
            >
              <option value="">-- Chọn khoa --</option>
              {wards.map(w => (
                <option key={w.wardId} value={w.wardId}>
                  {w.name} (Sức chứa: {w.capacity})
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          <div className="d-flex gap-2">
            <Button variant="primary" type="submit">
              {isEdit ? 'Cập nhật' : 'Tạo mới'}
            </Button>
            <Button variant="secondary" onClick={() => navigate('/nurses')}>
              Hủy
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}